import { Component, Input, ViewEncapsulation } from '@angular/core';
import {
  BaseFilterCellComponent,
  FilterService,
} from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { ResourceManager } from 'src/app/common/resource-manager';

@Component({
  selector: 'fts-grid-combo-filter-cell',
  templateUrl: './fts-grid-combo-filter-cell.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class FtsGridComboFilterCellComponent extends BaseFilterCellComponent {
  constructor(
    filterService: FilterService,
    private resourceManage: ResourceManager
  ) {
    super(filterService);
  }

  @Input() public filter!: CompositeFilterDescriptor;
  @Input() public data!: any[];
  @Input() public textField!: string;
  @Input() public valueField!: string;
  @Input() public FieldId!: string;

  public get selectedValue(): any {
    const filter = this.filterByField(this.valueField);
    return filter ? filter.value : null;
  }

  public onChange(value: any): void {
    this.applyFilter(
      value === null || value === undefined
        ? this.removeFilter(this.FieldId)
        : this.updateFilter({
            field: this.FieldId,
            operator: 'eq',
            value: value,
          })
    );
  }
}
