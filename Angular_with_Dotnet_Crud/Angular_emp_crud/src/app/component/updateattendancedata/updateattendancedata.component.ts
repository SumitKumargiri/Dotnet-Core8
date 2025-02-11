import { Component, OnInit } from '@angular/core';
import { DataService } from '../../service/data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-updateattendancedata',
  standalone: false,
  
  templateUrl: './updateattendancedata.component.html',
  styleUrl: './updateattendancedata.component.css'
})
export class UpdateattendancedataComponent implements OnInit {

  attendanceData = {
    empid: 0,
    firstname: '',
    lastname: '',
    timein: '',
    timeout: '',
    total: 0,
    status: ''
  };

  constructor(private dataService: DataService,private route: ActivatedRoute,private router: Router,private toastr: ToastrService) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const empid = params['empid'];
      if (empid) {
        this.getAttendanceById(empid);
      }
    });
  }

  getAttendanceById(empid: number): void {
    this.dataService.getEmployeeAttendance(empid).subscribe(
      (response) => {
        if (response.success) {
          this.attendanceData = response.data;
        }
      },
      (error) => {
        console.error('Error fetching attendance data:', error);
      }
    );
  }

  calculateAttendance() {
    const timeIn = new Date(this.attendanceData.timein).getTime();
    const timeOut = new Date(this.attendanceData.timeout).getTime();

    if (timeIn && timeOut && timeOut > timeIn) {
      const hoursWorked = (timeOut - timeIn) / (1000 * 60 * 60);
      this.attendanceData.total = parseFloat(hoursWorked.toFixed(2));

      if (hoursWorked <= 5) {
        this.attendanceData.status = 'Half Day';
      } else if (hoursWorked > 5 && hoursWorked <= 8) {
        this.attendanceData.status = 'Short Leave';
      } else {
        this.attendanceData.status = 'Full Day';
      }
    }
  }

  updateAttendance() {
    if (!this.attendanceData.firstname || !this.attendanceData.lastname) {
      // alert('Please enter First Name and Last Name');
      this.toastr.error('Please enter First Name and Last Name');
      return;
    }

    this.calculateAttendance();

    this.dataService.updateAttendance(this.attendanceData.firstname, this.attendanceData.lastname, this.attendanceData)
      .subscribe(response => {
        if (response.success) {
          this.toastr.success('Attendance updated successfully!');
          this.router.navigate(['/']);  
        } else {
          this.toastr.error('Failed to update attendance.');
        }
      }, error => {
        console.error('Error updating attendance:', error);
        this.toastr.error('An error occurred while updating attendance.');
      });
  }
}
