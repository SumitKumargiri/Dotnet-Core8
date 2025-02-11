import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListdataComponent } from './component/listdata/listdata.component';
import { OuterlayoutComponent } from './component/layout/outerlayout/outerlayout.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AdddataComponent } from './component/adddata/adddata.component';
import { UpdatedataComponent } from './component/updatedata/updatedata.component';
import { DeletedataComponent } from './component/deletedata/deletedata.component';
import { AttendancedataComponent } from './component/attendancedata/attendancedata.component';
import { UpdateattendancedataComponent } from './component/updateattendancedata/updateattendancedata.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    ListdataComponent,
    OuterlayoutComponent,
    AdddataComponent,
    UpdatedataComponent,
    DeletedataComponent,
    AttendancedataComponent,
    UpdateattendancedataComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule, 
    ToastrModule.forRoot(),
  ],
  providers: [
    provideClientHydration(withEventReplay()),
    provideAnimations(), 
    provideToastr(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
