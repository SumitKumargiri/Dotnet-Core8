import { Component, OnInit } from '@angular/core';
import { DataService } from '../../service/data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-updatedata',
  standalone: false,
  templateUrl: './updatedata.component.html',
  styleUrl: './updatedata.component.css'
})
export class UpdatedataComponent implements OnInit {

  empid: number = 0;
  employee: any = {};  
  message: string = '';

  constructor(private route: ActivatedRoute, private dataService: DataService,private toastr: ToastrService,private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.empid = params['id'];
      if (this.empid) {
        this.getEmployeeById(this.empid);
      }
    });
  }

  getEmployeeById(id: number) {
    this.dataService.getEmployeeById(id).subscribe(response => {
      if (response.success && response.data.length > 0) {
        let empData = response.data[0]; 
        if (empData.dob) {
          empData.dob = new Date(empData.dob).toISOString().split('T')[0];
        }
        this.employee = { ...empData };  
      }
    });
  }
  
  updateEmployee() {
    if (this.employee.dob) {
      this.employee.dob = new Date(this.employee.dob).toISOString();
    }
    this.dataService.updateEmployee(this.employee).subscribe(response => {
      if (response.success) {
        this.toastr.success('Employee updated successfully!');
        this.router.navigate(['/']); 
      } else {
        this.toastr.error('Failed to update employee!');
      }
    });
  }
}
