import { FilterGroup } from "./filter";
import { Sort } from "./sort";

export interface PagingParam{
    PageIndex: number;
    PageSize: number;
    FilterGroups: FilterGroup[] | undefined;
    Sorts: Sort[] | undefined;
    FilterFields: string[] | undefined;
    SumaryFields: string[] | undefined;
    TranId: string | '';
}