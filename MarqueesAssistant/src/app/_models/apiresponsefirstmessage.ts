import { Message } from "./message";

export interface Apiresponsefirstmessage {

    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
    hasPrevious: boolean;
    hasNext: boolean;
    data?: Message[];
    nameOfUser: string;

}
