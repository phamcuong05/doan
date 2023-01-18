export interface Filter{
    Field: string;
    Operator: string;
    Value: any;
}

export interface FilterGroup{
    Filters: Filter[],
    Logic: string
}