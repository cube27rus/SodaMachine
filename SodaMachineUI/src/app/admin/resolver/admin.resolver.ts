import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AdminService } from '../../services/data-services/admin.service';

@Injectable()
export class AdminResolver implements Resolve<any> {
    constructor(private router: Router,
        private adminService: AdminService) {
    }

    public resolve(route: ActivatedRouteSnapshot) {
        this.adminService.checkSecret(route.params.secret).subscribe(
            data => {
                console.log('ok');
            },
            error => {
                this.router.navigate(['/']);
            }
        );
    }
}
