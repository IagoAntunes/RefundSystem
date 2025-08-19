export interface RefundResponse {
    success: boolean;
    data: Refund;
}

export interface RefundListResponse {
    success: boolean;
    data: Refund[];
}

export interface Refund {
    id: string;
    name: string;
    value: number;
    status: number;
    imageId: string;
    categoryId: string;
    userId: string;
    category: any | null;
    userName: string;
}