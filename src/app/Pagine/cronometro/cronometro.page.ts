import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit } from '@angular/core';
import { createAnimation } from '@ionic/angular';

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
  translate: TranslateService

  constructor(translate: TranslateService) {
    this.translate = translate
  }

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

  async reset() {
    this.milliseconds = 0
    this.seconds = 0
    this.minutes = 0
    this.timeHolder.innerText = '00:00:00'
    const anim = createAnimation()
      .addElement(this.giri)
      .duration(500)
      .fromTo('opacity', '0.5', '0')
      .onFinish(async () => {
        this.opacityAnimation(this.giri, '0', '1')
      })
    await anim.play()
    this.giri.innerHTML = ''
  }

  addGiro() {
    const item = document.createElement('ion-item')
    const label = document.createElement('ion-label')
    label.textContent = this.timeHolder.innerText
    item.color = 'secondary'
    item.appendChild(label)
    item.classList.add('ion-text-center')
    this.giri.appendChild(item)
    this.opacityAnimation(item, '0.3', '1')
  }

  async opacityAnimation(element: HTMLElement, from: string, to: string, duration: number = 500) {
    const animation = createAnimation()
      .addElement(element)
      .duration(duration)
      .fromTo('opacity', from, to)
    await animation.play()
  }

}
