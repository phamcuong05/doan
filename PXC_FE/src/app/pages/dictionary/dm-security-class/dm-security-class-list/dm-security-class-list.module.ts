import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmSecurityClassListComponent } from './dm-security-class-list.component';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmSecurityClassDetailModule } from '../dm-security-class-detail/dm-security-class-detail.module';



@NgModule({
  declarations: [
    DmSecurityClassListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmSecurityClassListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmSecurityClassDetailModule
  ]
})
export class DmSecurityClassListModule { }
