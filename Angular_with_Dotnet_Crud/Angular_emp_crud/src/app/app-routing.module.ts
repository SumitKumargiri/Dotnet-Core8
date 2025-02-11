import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OuterlayoutComponent } from './component/layout/outerlayout/outerlayout.component';
import { ListdataComponent } from './component/listdata/listdata.component';
import { AdddataComponent } from './component/adddata/adddata.component';
import { UpdatedataComponent } from './component/updatedata/updatedata.component';
import { DeletedataComponent } from './component/deletedata/deletedata.component';
import { UpdateattendancedataComponent } from './component/updateattendancedata/updateattendancedata.component';

const routes: Routes = [
  {
    path:'',
    component:OuterlayoutComponent,
    children:[
      {path:'listdata',component:ListdataComponent},
      {path:'adddata',component:AdddataComponent},
      {path:'updatedata',component:UpdatedataComponent},
      {path:'deletedata',component:DeletedataComponent},
      {path:'updateattendance/:empid',component:UpdateattendancedataComponent},
      { path:'',redirectTo:'listdata',pathMatch:'full'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
