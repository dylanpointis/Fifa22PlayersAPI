import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import {MatCardModule} from '@angular/material/card'; //esto es de material angular para tener cards
import {MatButtonModule} from '@angular/material/button'; //para los botones
import {MatIcon, MatIconModule} from '@angular/material/icon';
import { APIService } from '../../Services/apiservice.service';
import { inject, Injectable } from '@angular/core';
import { FootballPlayer } from '../../Models/FootballPlayer';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-player-card',
  imports: [MatCardModule, MatButtonModule, CommonModule, MatIconModule],
  templateUrl: './player-card.component.html',
  styleUrl: './player-card.component.css'
})


export class PlayerCardComponent {
  @Input() currentPlayer!: FootballPlayer;
  public headercolor: string = '';
  public color: string = '';
  private APIService: APIService = inject(APIService)


  //no se puede usar el constructor porque this.player todavia no esta definido
  //  cuando se lo llama. Hay que usar ngOnInit para que este definido
  ngOnInit(): void {
    if (this.currentPlayer.overall >= 80) {
      this.headercolor = "rgb(226, 220, 32)"
      this.color = "rgb(247, 247, 77)"
    } else if (this.currentPlayer.overall >= 70 && this.currentPlayer.overall < 80) {
      this.headercolor = "silver"
      this.color = "light-gray";
    } else {
      this.color = "rgb(233, 188, 66)";
      this.headercolor = "rgb(190, 152, 46)";
    }
  }

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}


    defaultImage(event: any): void {
      console.log('Error loading image. Id: ' + this.currentPlayer.playerID);
    event.target.src = 'public/defaultplayer_img.png'; //default image in case it doesn't exists
  }
}
