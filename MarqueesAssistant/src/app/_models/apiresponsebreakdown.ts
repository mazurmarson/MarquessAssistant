import { Breakdown } from './breakdown';

export interface Apiresponsebreakdown {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    hasPrevious: boolean;
    hasNext: boolean;
    data?: Breakdown[];
}
