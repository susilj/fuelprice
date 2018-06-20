import Vue from 'vue';
import { Component } from 'vue-property-decorator';
// import bTable from 'bootstrap-vue/es/components/table/table';

interface Fuel {
    company: string;
    effectiveDate: Date;
    city: string;
    fuelTypeWithPrice: [FuelType];
}

interface FuelType {
    type: string;
    price: number;
}

// @Component({
//     template: './home.vue.html',
//     'b-table': bTable
// })
@Component
export default class HomeComponent extends Vue {
    fuel: Fuel[] = [];
    isBusy: boolean = false;

    data() {
        return {
            fuel: [],
            // appTitle: 'Testing',
            // isBusy: false
        }
    }
    mounted() {
        fetch('api/Fuel/FuelPrice/')
            .then(response => response.json() as Promise<Fuel[]>)
            .then(data => {
                this.fuel = data;
                // console.info(this.fuel);
            });
    }
    // methods() {
        // let self = this;
        // function
    //      myProvider(ctx: any) {
    //         console.info('myProvider');
    //         // Here we don't set isBusy prop, so busy state will be handled by table itself
    //         // self.isBusy = true
    //         this.isBusy = true
    //         fetch('api/SampleData/FuelPrice/')
    //             .then(response => response.json() as Promise<Fuel[]>)
    //             .then(data => {
    //                 // self.fuel = data;
    //                 this.fuel = data;
    //                 // console.info(self.fuel);
    //                 // console.info(this.fuel)
    //             })
    //             .catch(error => {
    //                 // Here we could override the busy state, setting isBusy to false
    //                 // self.isBusy = false
    //                 this.isBusy = true
    //                 // Returning an empty array, allows table to correctly handle busy state in case of error
    //                 return []
    //             });
    //     // }
    // }
}