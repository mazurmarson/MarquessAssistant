export interface Apiresponseplace {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    hasPrevious: boolean;
    hasNext: boolean;
    data?: data[];
}

export interface data {
    id: number;
    number: number;
    street: string;
    town: string;
    firstGradeDivision: string;
    secondGradeDivision: string;
    thirdGradeDivision: string;
    postCode: string;
}
