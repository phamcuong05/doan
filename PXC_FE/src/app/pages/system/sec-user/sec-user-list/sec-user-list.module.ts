import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SecUserListComponent } from './sec-user-list.component';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { SecUserDetailModule } from '../sec-user-detail/sec-user-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [SecUserListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: SecUserListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    SecUserDetailModule
  ],
  exports:[RouterModule,SecUserListComponent],
  providers:[WindowService, WindowContainerService],
})
export class SecUserListModule { }
