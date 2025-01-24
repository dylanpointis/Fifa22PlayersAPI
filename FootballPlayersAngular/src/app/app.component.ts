import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import {MatCardModule} from '@angular/material/card'; //esto es de material angular para tener cards
import {MatButtonModule} from '@angular/material/button'; //para los botones
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { APIService } from './Services/apiservice.service';
import { inject, Injectable } from '@angular/core';
import { FootballPlayer } from './Models/FootballPlayer';
import { PlayerCardComponent } from "./Pages/player-card/player-card.component";
import { CommonModule } from '@angular/common'; //hay que importar esto para usar *ngFor

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatCardModule, MatButtonModule, PlayerCardComponent,CommonModule, MatInputModule, FormsModule, MatSelectModule,MatIconModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})



export class AppComponent {

  private ApiService = inject(APIService)

  public playersList: FootballPlayer[] = [];
  public pageNumber: number = 1;
  public inputPageNumber: number = 1;
  public selectedOption: string = "name"; //name as default
  public inputSearchBar: any;
  title = 'FootballPlayersAngular';

constructor(private router: Router){
  this.obtener()
}

obtener(){
  this.ApiService.listPlayers(this.pageNumber).subscribe({
    next: data =>{
      if(data.length > 0){
        this.playersList = data
      }
      else{
        alert("Could not find the players list. Executing ETL. Try Again")
      }
    }
  })
}


pageUp() {
  this.pageNumber ++;
  this.inputPageNumber = this.pageNumber; // Sincroniza inputPageNumber con pageNumber
  this.obtener();
}

pageDown() {
  if (this.pageNumber > 1) {
      this.pageNumber--;
      this.inputPageNumber = this.pageNumber; // Sincroniza inputPageNumber con pageNumber
      this.obtener();
  }
}

setPage() {
  if (this.inputPageNumber >= 1) {
      this.pageNumber = this.inputPageNumber;
      this.obtener();
  }
}

returnHome(){
  this.pageNumber = 1;
  this.inputPageNumber = this.pageNumber;
  this.obtener()
  this.router.navigate(["/"]);
}

isDigit(value: any){
  return !isNaN(Number(value));
}



searchPlayer(option: string){
  switch(option){
    case "overall": {
      if (this.isDigit(this.inputSearchBar)) {
        this.filterByOverall();
    }
        break;
    }
    case "name": {
      this.filterByName()
      break;
    }
    case "nation": {
      this.filterByNationality()
      break;
    }
    case "club": {
      this.filterByClub()
      break;
    }
    default:{
      break;
    }
  }
}

filterByName(){
  this.ApiService.filterByName(this.inputSearchBar).subscribe({
    next: data =>{
      if(data.length > 0){
        this.playersList = data
      }
      else{
        alert("Could not find the filtered players")
      }
    }
  })
}


filterByNationality(){
  this.ApiService.filterByNationality(this.inputSearchBar).subscribe({
    next: data =>{
      if(data.length > 0){
        this.playersList = data
      }
      else{
        alert("Could not find the filtered players")
      }
    }
  })
}


filterByOverall(){
  this.ApiService.filterByOverall(this.inputSearchBar).subscribe({
    next: data =>{
      if(data.length > 0){
        this.playersList = data
      }
      else{
        alert("Could not find the filtered players")
      }
    }
  })
}


filterByClub(){
  this.ApiService.filterByClub(this.inputSearchBar).subscribe({
    next: data =>{
      if(data.length > 0){
        this.playersList = data
      }
      else{
        alert("Could not find the filtered players")
      }
    }
  })
}


loadComponent(p: FootballPlayer){
 // this.router.navigate(['/detail-card'], { queryParams: { player: JSON.stringify(p) } });
 this.router.navigate(['/detail-card', p.playerID]);
}


}
