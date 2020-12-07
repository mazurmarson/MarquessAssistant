import { Equipment } from './equipment';

export interface Apiresponseequipment {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    hasPrevious: boolean;
    hasNext: boolean;
    data?: Equipment[];

}
