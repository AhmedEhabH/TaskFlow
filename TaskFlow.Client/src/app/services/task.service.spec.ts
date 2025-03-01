import {
    HttpTestingController,
    provideHttpClientTesting
} from '@angular/common/http/testing';
import {TestBed} from '@angular/core/testing';

import {environment} from '../../environments/environment';
import {Task} from '../interfaces/task';

import {TaskService} from './task.service';
import { provideHttpClient } from '@angular/common/http';

describe('TaskService', () => {
    let service: TaskService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                provideHttpClient(), // ✅ Ensures HttpClient is available
                provideHttpClientTesting(), // ✅ Properly registers the testing backend
                TaskService,
                { provide: 'API_URL', useValue: environment.apiUrl }
            ]
        });

        service = TestBed.inject(TaskService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    it('should fetch tasks', () => {
        const dummyTasks: Task[] =
            [ {id : 1, title : 'Test Task', isCompleted : false} ];

        service.getTasks().subscribe(
            tasks => { expect(tasks).toEqual(dummyTasks); });

        const req = httpMock.expectOne(`${environment.apiUrl}/Task`);
        expect(req.request.method).toBe('GET');
        req.flush(dummyTasks);
    });

    it('should create a task', () => {
        const newTask: Task = {title : 'New Task', isCompleted : false};

        service.createTask(newTask).subscribe(
            task => { expect(task).toEqual({...newTask, id : 1}); });

        const req = httpMock.expectOne(`${environment.apiUrl}/Task`);
        expect(req.request.method).toBe('POST');
        req.flush({...newTask, id : 1});
    });

    afterEach(() => { httpMock.verify(); });
});
