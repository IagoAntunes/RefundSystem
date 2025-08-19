import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RefundListResponse, RefundResponse } from '../../../interfaces/refund';

@Injectable({
    providedIn: 'root'
})
export class RefundService {
    private apiUrl = 'https://localhost:7102/api/Refund'; 
    private imageUrl = 'https://localhost:7102/api/Images'; 

    constructor(private http: HttpClient) { }

    createRefund(formData: FormData): Observable<RefundResponse> {
        return this.http.post<RefundResponse>(this.apiUrl, formData);
    }

    getRefunds(): Observable<RefundListResponse> {
        return this.http.get<RefundListResponse>(this.apiUrl);
    }

    getRefundById(id: string): Observable<RefundResponse> {
        return this.http.get<RefundResponse>(`${this.apiUrl}/${id}`);
    }

    getReceiptImage(imageId: string): Observable<Blob> {
        return this.http.get(`${this.imageUrl}/${imageId}`, {
            responseType: 'blob'
        });
    }

}