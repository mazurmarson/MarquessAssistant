export interface Apiresponse {
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
    login: string;
    firstName?: any;
    lastName?: any;
}


