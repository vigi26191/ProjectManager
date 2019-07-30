import { Injectable } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
declare let alertify: any;

@Injectable({
    providedIn: 'root'
})
export class AlertifyService {
    private toastrOptions = {
        showCloseButton: true,
        toastTimeout: 10000,
        position: 'top-right'
    };

    constructor(public toastr: ToastrManager) { }

    confirm(message: string, okCallback: () => any) {
        alertify.confirm(message, function (e) {
            if (e) {
                okCallback();
            }
        }).set({ title: 'Project Manager' });
    }

    success(message) {
        this.clear();
        this.toastr.successToastr(message, 'Success!', this.toastrOptions);
    }

    error(message) {
        this.clear();
        this.toastr.errorToastr(message, 'Alert!', this.toastrOptions);
    }

    warning(message) {
        this.clear();
        this.toastr.warningToastr(message, 'Alert!', this.toastrOptions);
    }

    message(message) {
        this.clear();
        this.toastr.infoToastr(message, 'Info!', this.toastrOptions);
    }

    clear(): void {
        let toastr = $('#toastr-container');

        if (toastr && toastr.length > 0) {
            if (toastr[0].children.length > 0) {
                if (toastr[0].children[0].childNodes.length > 1) {
                    let closeButton: any = toastr[0].children[0].childNodes[1];
                    closeButton.click();
                }
            }
        }
    }
}
