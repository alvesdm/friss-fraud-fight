import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-calculate-probability',
  templateUrl: './calculate-probability.component.html'
})
export class CalculateProbabilityComponent {
  private _baseUrl: string;

  public person: PersonModel;
  public statusMessage: string;

  constructor(
    private _httpClient: HttpClient,
    private _router: Router,
    @Inject('BASE_URL') baseUrl: string)
  {
    this._baseUrl = baseUrl;
    this.person = {} as PersonModel;

    this.setStatusMessage(CalculateStatusMessages.NotStarted);
  }

  public calculateFraudProbability() {
    this.setStatusMessage(CalculateStatusMessages.Calculating);

    let personModel = { ...this.person };
    personModel.dateOfBirth = this.person.dateOfBirth.split("/").reverse().join("-");

    this._httpClient.post(this._baseUrl + 'api/fraud/calculate', personModel).subscribe(result => {
      const matchingResult = result as MatchingResult;
      let message = CalculateStatusMessages.Calculated
        .replace("$PERCENT$", matchingResult.probability.toString())
        .replace("$PERSON$", matchingResult.name.toString());

      this.setStatusMessage(message);
    }, error => {
      this.setStatusMessage(CalculateStatusMessages.Calculating);
      //console.error(error)
    });
  }

  private setStatusMessage(statusMessage: string) {
    this.statusMessage = statusMessage;
  }
}

interface PersonModel {
  dateOfBirth: string;
  identificationNumber: string;
  firstName: string;
  lastName: string;
}

interface MatchingResult {
  probability: number;
  name: string;
}

enum CalculateStatusMessages{
  NotStarted = "Fill out the form and click Calculate.",
  Calculating = "Calculating",
  Calculated = 'This person has a $PERCENT$% matching probability with $PERSON$.',
  Error = "Something went wrong. Check the form data and try again."
}


