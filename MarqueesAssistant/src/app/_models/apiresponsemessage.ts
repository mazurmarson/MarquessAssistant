import { Message } from "./message";

export interface Apiresponsemessage {
    
        currentPage: number;
        totalPages: number;
        pageSize: number;
        totalCount: number;
        hasPrevious: boolean;
        hasNext: boolean;
        data?: Message[];
    
    
    
}
