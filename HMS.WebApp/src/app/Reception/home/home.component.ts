import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { HomeService } from 'src/app/services/home.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material';
import { CheckReservationModalComponent } from './check-reservation-modal/check-reservation-modal.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private router: Router, translate: TranslateService, private homeService: HomeService,
    private toastr: ToastrService, public dialog: MatDialog) {
    translate.setDefaultLang('en');
    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('en');
  }

  ngOnInit() {
    this.homeService.getReservations().subscribe(response => {
      this.reservations = response;
    }
      , error => {
        this.toastr.error(error.message);
        console.log(error);
      });
  }

  reservations: any;

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['user/login']);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(CheckReservationModalComponent, {
      width: '800px',
      data: { }
    });

    dialogRef.afterClosed().subscribe(() => {
      
    });
  }
}
