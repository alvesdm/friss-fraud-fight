import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html'
})
export class AddPersonComponent {
  private _baseUrl: string;

  public person: AddPersonModel;

  constructor(
    private _httpClient: HttpClient,
    private _router: Router,
    @Inject('BASE_URL') baseUrl: string)
  {
    this._baseUrl = baseUrl;
    this.person = { } as AddPersonModel;
  }

  public addPerson() {
    let personModel = { ...this.person };
    personModel.dateOfBirth = this.person.dateOfBirth.split("/").reverse().join("-");

    this._httpClient.post(this._baseUrl + 'api/people', personModel).subscribe(result => {
      this._router.navigate(['']);
    }, error => console.error(error));
  }
}

interface AddPersonModel {
  dateOfBirth: string;
  identificationNumber: string;
  firstName: string;
  lastName: string;
}
