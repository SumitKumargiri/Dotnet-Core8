import { Component } from '@angular/core';
import { DataService } from '../../service/data.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-adddata',
  standalone: false,
  
  templateUrl: './adddata.component.html',
  styleUrl: './adddata.component.css'
})
export class AdddataComponent {

  employee = {
    empid: 0,
    firstname: '',
    lastname: '',
    dob: '',
    gender: '',
    qualification: '',
    email: '',
    phonenumber: null,
    isactive: true
  };

  message: string = '';

  constructor(private dataService: DataService,private toastr: ToastrService,private route: ActivatedRoute,private router: Router) {}

  insertEmployee() {
    this.dataService.insertEmployee(this.employee).subscribe(response => {
      console.log('Insert Response:', response);
      if (response.success) {
        this.toastr.success('Employee inserted successfully!');
        this.router.navigate(['/']); 
      } else {
        this.toastr.error('Failed to insert employee!');
      }
    }, error => {
      console.error('Insert Error:', error);
      this.toastr.error('Error inserting employee!');
    });
  }
}
