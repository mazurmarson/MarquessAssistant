import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationWordsService {

constructor() { }

readonly nextPageText = "Następna";
readonly previousPageText = "Poprzednia";
readonly sizesOfPage: number[] = [5,10,20,30,40,50];
}
