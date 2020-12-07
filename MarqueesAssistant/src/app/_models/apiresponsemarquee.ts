import { Marquee } from './marquee';

export interface Apiresponsemarquee {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    hasPrevious: boolean;
    hasNext: boolean;
    data?: Marquee[];
}
