import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../services/employeeservice.service'

@Component({
    selector: 'fetchemployee',
    templateUrl: './fetchemployee.component.html'
})

export class FetchEmployeeComponent {
    public emplList: Employee[];

    constructor(public http: HttpClient, private _router: Router, private _employeeService: EmployeeService) {
        this.getEmployees();
    }


    getEmployees() {
      this._employeeService.getEmployees().subscribe((data: Employee[]) => {
        this.emplList = data;
      });
    }

    delete(employeeID) {
        var ans = confirm("Do you want to delete customer with Id: " + employeeID);
        if (ans) {
            this._employeeService.deleteEmployee(employeeID).subscribe((data) => {
                this.getEmployees();
            }, error => console.error(error)) 
        }
    }
}
export interface Employee {
  myid: number;
  name: string;
  address: string;
  role: string;
  department: string;
  skillSets: string;
  date_of_Birth: string;
  date_of_Joining: string;
  isActive: boolean;
}
