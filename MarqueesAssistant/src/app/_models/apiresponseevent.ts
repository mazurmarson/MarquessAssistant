export interface Apiresponseevent {
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
    name: string;
    startDate: Date;
    endDate: Date;
    placeId: number;
    placeName: string;
    typeOfEvent: string;
}