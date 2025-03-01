import {provideHttpClient} from '@angular/common/http';
import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import {provideRouter} from '@angular/router';

import {environment} from '../environments/environment';

import {routes} from './app.routes';

export const appConfig: ApplicationConfig = {
    providers : [
        provideZoneChangeDetection({eventCoalescing : true}),
        provideRouter(routes), provideHttpClient(),
        {provide : 'API_URL', useValue : environment.apiUrl}
    ]
};
