import { Component, Input, OnChanges, SimpleChanges, inject } from '@angular/core';
import { FootballPlayer } from '../../Models/FootballPlayer';
import { ActivatedRoute } from '@angular/router';
import { NgIf } from '@angular/common';
import { APIService } from '../../Services/apiservice.service';
import {MatCard, MatCardModule} from '@angular/material/card'

@Component({
  selector: 'app-detail-card',
  imports: [NgIf, MatCardModule],
  templateUrl: './detail-card.component.html',
  styleUrl: './detail-card.component.css'
})


export class DetailCardComponent implements OnChanges {
  public currentPlayer!: FootballPlayer;
  @Input('id') idPlayer!: number;
  apiService = inject(APIService);

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.loadPlayer();
  }

  //This method is called when the input changes. It is used to load the player data.
  //Se usa cuando clickeo a otro jugador y se actualiza el id del jugador.
  ngOnChanges(changes: SimpleChanges) {
    if (changes['idPlayer']) {
      this.loadPlayer();
    }
  }

  private loadPlayer() {
    if (this.idPlayer) {
      this.apiService.getPlayer(this.idPlayer).subscribe({
        next: data => {
          if (data) {
            this.currentPlayer = data;
          } else {
            console.error('No se encontró el jugador');
          }
        }
      });
    }
  }


  defaultImage(event: any): void {
    event.target.src = 'public/defaultplayer_img.png'; //default image in case it doesn't exists
  }


}

    /*
    this.route.queryParams.subscribe(params => {
      if (params['player']) {
        this.currentPlayer = JSON.parse(decodeURIComponent(params['player']));
      }
    });*/
  


