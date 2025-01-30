import { Credentials } from "./Credentials";
import { SubscriptionState } from "./Enums/SubscriptionState";
import { SecurityQuestion } from "./SecurityQuestions";

export class User {
    public id: string = ""
    public username: string = "";
    public name: string = "";
    public surname: string = "";
    public email: string = "";
    public isTrainer: boolean = false;
    public private: boolean = false;
    public credentials: Credentials = new Credentials("", "");
    public securityQuestions: SecurityQuestion[] = [];
    public subscriptionState: SubscriptionState = SubscriptionState.Undefined;
    public description: string = '';
}
