import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

import {Task} from '../interfaces/task';
import { environment } from '../../environments/environment';

@Injectable({providedIn : 'root'})
export class TaskService {
    private apiUrl: string = `${environment.apiUrl}/Task`;
    constructor(private http: HttpClient) {}

    getTasks(): Observable<Task[]> {
        return this.http.get<Task[]>(this.apiUrl);
    }

    createTask(task: Task): Observable<Task> {
        return this.http.post<Task>(this.apiUrl, task);
    }
}
