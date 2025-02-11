import { Component, OnInit } from '@angular/core';
import { DataService } from '../../service/data.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-listdata',
  standalone: false,
  
  templateUrl: './listdata.component.html',
  styleUrl: './listdata.component.css'
})
export class ListdataComponent implements OnInit {

  employees: any[] = [];
  message: string = '';

  constructor(private dataService: DataService,private router: Router,private toastr: ToastrService) {}

  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.dataService.getEmployees().subscribe(response => {
      if (response.success) {
        this.employees = response.data;
      }
    });
  }

  editEmployee(empid: number) {
    this.router.navigate(['/updatedata'], { queryParams: { id: empid } });
  }

  deleteEmployee(empid: number) {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.dataService.deleteEmployee(empid).subscribe(response => {
        if (response.success) {
          // this.message = 'Employee deleted successfully!';
          this.toastr.success('Employee deleted successfully!');
          this.getEmployees();  
        } else {
          this.message = 'Failed to delete employee!';
          this.toastr.error('Failed to delete employee!');
        }
      }, error => {
        console.error('Delete Error:', error);
        this.message = 'An error occurred while deleting employee.';
        this.toastr.error('An error occurred while deleting employee.');
      });
    }
  }
}
