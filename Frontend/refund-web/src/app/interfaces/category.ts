export interface CategoryResponse {
    success: boolean;
    data: Category[];
}

export interface Category {
    id: string;
    name: string;
}