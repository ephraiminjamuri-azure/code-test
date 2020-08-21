import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Message } from 'primeng/components/common/api';
import { CheckboxModule } from 'primeng/checkbox';
import { MessageService } from 'primeng/components/common/messageservice';
import { FetchEmployeeComponent } from '../fetchemployee/fetchemployee.component';
import { EmployeeService } from '../services/employeeservice.service';
//import { GenericLookupService } from '../services/genericLookupservice.service';
//imports section
@Component({
  selector: 'createemployee',
  templateUrl: './AddEmployee.component.html'
})

export class createemployee implements OnInit {
  employeeForm: FormGroup;
  title: string = "Create";
  id: number;
  errorMessage: any;
  msgs: Message[] = [];
  checked: boolean = true;
  //declarations section
  //private _genericLookupService: GenericLookupService,
  constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,    
    private _employeeService: EmployeeService, private _router: Router) {
    if (this._avRoute.snapshot.params["id"]) {
      this.id = this._avRoute.snapshot.params["id"];
    }

    this.employeeForm = this._fb.group({
      id: 0,
      name: [''],
      address: [''],
      role: [''],
      department: [''],
      skillSets: [''],
      date_of_Birth: [''],
      date_of_Joining: [''],
      isActive: [false],

    })
  }

  ngOnInit() {
    if (this.id > 0) {
      this.title = "Edit";
      this._employeeService.getEmployeeById(this.id)
        .subscribe(resp => this.employeeForm.setValue(resp)
          , error => this.errorMessage = error);
    }
    //inside ngOnInit

  }
  //outside ngOnInit


  save() {

    if (!this.employeeForm.valid) {
      return;
    }

    if (this.title == "Create") {
      this._employeeService.saveEmployee(this.employeeForm.value)
        .subscribe((res: any) => {
          if (res.status == "Error") {
            this.msgs = [];
            this.msgs.push({ severity: 'error', summary: 'Error Message', detail: res.message });
          }
          else {
            this._router.navigate(['/fetch-employee']);
          }
        }, error => this.errorMessage = error)
    }
    else if (this.title == "Edit") {
      this._employeeService.updateEmployee(this.employeeForm.value)
        .subscribe((res: any) => {
          if (res.status == "Error") {
            this.msgs = [];
            this.msgs.push({ severity: 'error', summary: 'Error Message', detail: res.message });
          }
          else {
            this._router.navigate(['/fetch-employee']);
          }
        }, error => this.errorMessage = error)
    }
  }

  cancel() {
    this._router.navigate(['/fetch-employee']);
  }

  get name() { return this.employeeForm.get('name'); }
  get address() { return this.employeeForm.get('address'); }
  get role() { return this.employeeForm.get('role'); }
  get department() { return this.employeeForm.get('department'); }
  get skillSets() { return this.employeeForm.get('skillSets'); }
  get date_of_Birth() { return this.employeeForm.get('date of Birth'); }
  get date_of_Joining() { return this.employeeForm.get('date of Joining'); }
  get isActive() { return this.employeeForm.get('isActive'); }
}
