import {Directory, Encoding, Filesystem} from '@capacitor/filesystem'

export class DatabaseHandler<Type> {

    filename: string
    directory: Directory
    encoding: Encoding

    constructor(filename: string, directory: Directory = undefined, encoding: Encoding = undefined) {
        this.filename = filename
        if (directory !== undefined) {
            this.directory = directory
        } else {
            this.directory = Directory.Data
        }
        if (encoding !== undefined) {
            this.encoding = encoding
        } else {
            this.encoding = Encoding.UTF8
        }
    }

    public initDatabase() {
        console.log('Database initialized')
        Filesystem.writeFile({
            data: '',
            path: this.filename,
            directory: this.directory,
            encoding: this.encoding
        })
    }

    public async readDatabase(): Promise<Type[]> {
        const result = await Filesystem.readFile({
            path: this.filename,
            directory: this.directory,
            encoding: this.encoding
        })
        if (result !== undefined) {
            const data = JSON.parse('[' + result.data + ']')
            console.log('Read from', this.filename, '\n', data)
            return data
        } else {
            console.error('Error on reading database', this.filename)
            return undefined
        }
    }

    public async loadSpecificElement(element: Type) {
        const database = await this.readDatabase()
        for (let i = 0; i < database.length; i++) {
            if (database[i] === element) {
                return database[i]
            }
        }
        return undefined
    }

    public writeDatabase(newDatabase: Type[]) {
        let DBStringified: string = JSON.stringify(newDatabase)
        Filesystem.writeFile({
            data: DBStringified.substr(1, DBStringified.length - 2),
            path: this.filename,
            directory: this.directory,
            encoding: this.encoding
        })
            .then(() => console.log('Database updated with', newDatabase))
            .catch(() => console.error('Error updating', this.filename))
    }

    public async add(element: Type) {
        const database = await this.readDatabase()
        const index = this.getIndex(database, element)
        if (index === -1) {
            const database = (await this.readDatabase()).concat(element)
            this.writeDatabase(database)
        } else {
            this.modify(database[index], element)
        }
    }

    public async modify(oldObject: Type, newObject: Type) {
        let database = await this.readDatabase()
        const index = this.getIndex(database, oldObject)
        database.splice(index, 1)
        this.writeDatabase(database.concat(newObject))
    }

    public async delete(element: Type) {
        let database = await this.readDatabase()
        const index = this.getIndex(database, element)
        if (index === -1) {
            console.error('Element not found, deletion did not happen')
        } else {
            database.splice(index, 1)
            this.writeDatabase(database)
        }
    }

    public async drop() {
        await Filesystem.deleteFile({
            path: this.filename,
            directory: this.directory
        })
    }
    
    private getIndex(array: Type[], element: Type): number {
        for (let cont = 0; cont < array.length; cont++) {
            if (this.compareObjects(array[cont], element)) {
                return cont
            }
        }
        return -1
    }

    private compareObjects(object1, object2) {
        const keys1 = Object.keys(object1)
        const keys2 = Object.keys(object2)
        if (keys1.length !== keys2.length) {
            return false
        } else {
            for (let key of keys1) {
                if (object1[key] !== object2[key]) {
                    return false
                }
            }
            return true
        }
    }

}