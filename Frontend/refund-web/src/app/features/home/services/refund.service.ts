import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RefundResponse } from '../../../interfaces/refund';

@Injectable({
    providedIn: 'root'
})
export class RefundService {
    private apiUrl = 'https://localhost:7102/api/Refund'; 

    constructor(private http: HttpClient) { }

    createRefund(formData: FormData): Observable<RefundResponse> {
        return this.http.post<RefundResponse>(this.apiUrl, formData);
    }
}