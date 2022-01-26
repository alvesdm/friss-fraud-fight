import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-matching-rule',
  templateUrl: './update-matching-rule.component.html'
})
export class UpdateMatchingRuleComponent {
  private _baseUrl: string;

  public matchingRule: MatchingRuleModel;

  constructor(
    private _httpClient: HttpClient,
    private _router: Router,
    @Inject('BASE_URL') baseUrl: string)
  {
    this._baseUrl = baseUrl;

    this.setDefaultMathcingRules();
    this.loadSavedMatchingRule();
  }

  public saveMatchingRules() {
    this._httpClient.put(this._baseUrl + 'api/settings', this.matchingRule).subscribe(result => {
      this._router.navigate(['']);
    }, error => console.error(error));
  }

  private loadSavedMatchingRule() {
    this._httpClient.get<MatchingRuleModel>(this._baseUrl + 'api/settings').subscribe(result => {
      this.matchingRule = result as MatchingRuleModel;
    }, error => {
      console.log(error);
    });
  }

  private setDefaultMathcingRules() {
    this.matchingRule = {
      dateOfBirthSamePercent: 40,
      firstNameSamePercent: 20,
      firstNameSimilarPercent: 15,
      lastNameSamePercent: 40
    } as MatchingRuleModel;
  }
}

interface MatchingRuleModel {
  lastNameSamePercent: number;
  firstNameSamePercent: number;
  firstNameSimilarPercent: number;
  dateOfBirthSamePercent: number;
}
