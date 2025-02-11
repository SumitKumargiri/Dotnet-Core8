import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  apiUrl = 'https://localhost:7025';
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Crudemp/getempdata`);
  }

  insertEmployee(employee: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Crudemp/insertempdata`, employee);
  }

  getEmployeeById(empid: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Crudemp/getempdata/${empid}`);
  }

  updateEmployee(employee: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/Crudemp/updateempdata`, employee);
  }
  deleteEmployee(empid: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/Crudemp/deleteempdata/${empid}`);
  }

  getAttendanceData(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Crudemp/getattendancedata`);
  }

  getEmployeeAttendance(empid: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Crudemp/getemployeeattendancedata/${empid}`);
  }

  updateAttendance(firstname: string, lastname: string, attendanceData: any): Observable<any> {
    const url = `${this.apiUrl}/Crudemp/updateattendancedata?firstname=${firstname}&lastname=${lastname}`;
    return this.http.put<any>(url, attendanceData);
  }
}
