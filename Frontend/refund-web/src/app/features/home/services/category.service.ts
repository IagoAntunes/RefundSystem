import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoryResponse } from '../../../interfaces/category';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    // TODO: MOVE TO ENV
    private apiUrl = 'https://localhost:7102/api/Category'; 

    constructor(private http: HttpClient) { }

    getAllCategories(): Observable<CategoryResponse> {
    return this.http.get<CategoryResponse>(`${this.apiUrl}`);
    }

}