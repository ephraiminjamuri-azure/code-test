import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { catchError } from 'rxjs/operators';

@Injectable()
export class EmployeeService {
    myAppUrl: string = "";

    constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getEmployees() {
        return this._http.get(this.myAppUrl + 'api/Employee/Index');
    }

    getEmployeeById(id: number) {
        return this._http.get(this.myAppUrl + "api/Employee/Details/" + id);
    }

    saveEmployee(employee) {
          return this._http.post(this.myAppUrl + 'api/Employee/Create', employee);
      }

    updateEmployee(employee) {
        return this._http.put(this.myAppUrl + 'api/Employee/Edit', employee);
    }

    deleteEmployee(id) {
        return this._http.delete(this.myAppUrl + "api/Employee/Delete/" + id);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}
