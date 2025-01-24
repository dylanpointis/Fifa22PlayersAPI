import { Injectable, inject } from '@angular/core';
import { FootballPlayer } from '../Models/FootballPlayer';
import { HttpClient } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class APIService {

  private httpService = inject(HttpClient)
  private url = "http://localhost:5177/api/Players"
  //http://localhost:5177/api/Players/1
  constructor() { }

  playersList: FootballPlayer[] = []


  listPlayers(pageNumber: number){
    return this.httpService.get<FootballPlayer[]>(this.url + "/page/" + pageNumber)
  }

  getPlayer(playerID: number){
    return this.httpService.get<FootballPlayer>(`${this.url}/${playerID}`);
  }


  filterByName(nombre: string){
    return this.httpService.get<FootballPlayer[]>(this.url + "/name/" + nombre)
  }

  filterByNationality(nombre: string){
    return this.httpService.get<FootballPlayer[]>(this.url + "/nation/" + nombre)
  }

  
  filterByOverall(overall: number){
    return this.httpService.get<FootballPlayer[]>(this.url + "/overall/" + overall)
  }

  filterByClub(club: string){
    return this.httpService.get<FootballPlayer[]>(this.url + "/club/" + club)
  }

}
