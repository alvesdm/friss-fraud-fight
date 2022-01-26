import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { PersonModel } from './person-model';
import { MatchingResult } from './matching-result';
import { CalculateStatusMessages } from './calculate-status-messages';

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
    if (personModel.dateOfBirth !== undefined) {
      personModel.dateOfBirth = this.person.dateOfBirth.split("/").reverse().join("-");
    }

    this._httpClient.post(this._baseUrl + 'api/fraud/calculate', personModel).subscribe(result => {
      const matchingResult = result as MatchingResult;
      let message = CalculateStatusMessages.Calculated
        .replace("$PERCENT$", matchingResult.probability.toString())
        .replace("$PERSON$", matchingResult.name.toString());

      this.setStatusMessage(message);
    }, error => {
      this.setStatusMessage(CalculateStatusMessages.Calculating);
    });
  }

  private setStatusMessage(statusMessage: string) {
    this.statusMessage = statusMessage;
  }
}
