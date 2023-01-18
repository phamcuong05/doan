import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListPackageListComponent } from './list-package-list.component';
import { ListPackageDetailModule } from '../list-package-detail/list-package-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ListPackageListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListPackageListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListPackageDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListPackageListModule { }
