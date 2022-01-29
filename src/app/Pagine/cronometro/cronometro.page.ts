import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cronometro',
  templateUrl: './cronometro.page.html',
  styleUrls: ['./cronometro.page.scss'],
})
export class CronometroPage implements OnInit {

  running: boolean = false
  milliseconds = 0
  seconds = 0
  minutes = 0
  timeHolder: HTMLElement
  giri: HTMLElement

  constructor() { }

  ngOnInit() {
    this.timeHolder = document.getElementById('time')
    this.giri = document.getElementById('giri')
  }

  time = setInterval(() => {
      let m: any, s: any, M: any
      if (this.running == true) {
        this.milliseconds += 1
        if (this.milliseconds == 100) {
          this.seconds += 1
          this.milliseconds = 0
        }
        if (this.seconds == 60) {
          this.minutes += 1
          this.seconds = 0
        }
        if (this.milliseconds < 10) {
          m = '0' + this.milliseconds
        } else {
          m = this.milliseconds
        }
        if (this.seconds < 10) {
          s = '0' + this.seconds
        } else {
          s = this.seconds
        }
        if (this.minutes < 10) {
          M = '0' + this.minutes
        } else {
          M = this.minutes
        }
        this.timeHolder.innerText = M + ':' + s + ':' + m
      }
      
    }, 10);

  reset() {
    this.milliseconds = 0
    this.seconds = 0
    this.minutes = 0
    this.timeHolder.innerText = '00:00:00'
    this.giri.innerHTML = ''
  }

  addGiro() {
    const p = document.createElement('p')
    p.innerText = this.timeHolder.innerText
    this.giri.appendChild(p)
  }

}
