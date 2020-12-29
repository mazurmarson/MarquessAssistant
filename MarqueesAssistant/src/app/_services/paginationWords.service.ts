import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationWordsService {

constructor() { }

readonly nextPageText = "Następna";
readonly previousPageText = "Poprzednia";

}
