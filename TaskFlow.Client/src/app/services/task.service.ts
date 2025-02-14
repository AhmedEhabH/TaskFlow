import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

import {Task} from '../interfaces/task';

@Injectable({providedIn : 'root'})
export class TaskService {
    private apiUrl: string = 'https://localhost5001/api/Task'
    constructor(private http: HttpClient) {}

    getTasks(): Observable<Task[]> {
        return this.http.get<Task[]>(this.apiUrl);
    }

    createTask(task: Task): Observable<Task> {
        return this.http.post<Task>(this.apiUrl, task);
    }
}
