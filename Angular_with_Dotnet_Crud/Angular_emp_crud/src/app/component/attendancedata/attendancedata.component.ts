import { Component, OnInit } from '@angular/core';
import { DataService } from '../../service/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-attendancedata',
  standalone: false,
  
  templateUrl: './attendancedata.component.html',
  styleUrl: './attendancedata.component.css'
})
export class AttendancedataComponent implements OnInit {

  attendanceData: any[] = []; 

  constructor(private dataService:DataService,private router: Router) {}

  ngOnInit(): void {
    this.loadAttendanceData();
  }

  loadAttendanceData(): void {
    this.dataService.getAttendanceData().subscribe(
      (response) => {
        if (response.success) {
          // this.attendanceData = response.data; 
          this.attendanceData = response.data.filter((emp: any) => emp.isactive);
          
        }
      },
      (error) => {
        console.error('Error fetching attendance data:', error);
      }
    );
  }
  navigateToUpdateAttendance(emp: any) {
    this.router.navigate(['updateattendance', emp.empid]);
  }
  
}
