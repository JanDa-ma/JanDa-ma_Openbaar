import { element } from "protractor";
import { products } from "./../Products";
import { Component, OnInit } from "@angular/core";
import { Gesture, GestureController } from "@ionic/angular";
import { computeStackId } from "@ionic/angular/directives/navigation/stack-utils";
import { compileFactoryFunction } from "@angular/compiler";
@Component({
  selector: "app-TestPage",
  templateUrl: "./TestPage.component.html",
  styleUrls: ["./TestPage.component.scss"],
})
export class TestPageComponent implements OnInit {
  _products = products.sort(() => Math.random() - 0.5);
  firstnu;
  constructor() {}
  columnNumber: number = 1;

  ngOnInit() {
    let cell1;
    let cell2;
    let cell3;
    let rownumber = 1;
    let arrayNumbers = [];

    this._products.forEach((element) => {
      //add span
      const span = document.createElement("span");

      span.className = "cell";
      span.style.padding = "2rem";
      span.textContent = element.id.toString();
      span.id = "span" + element.id;
      span.addEventListener("click", function () {
        span.style.backgroundColor = "limegreen";
        if (localStorage.getItem("chosennumber1") == null) {
          localStorage.setItem("chosennumber1", span.id.toString());
        } else {
          localStorage.setItem("chosennumber2", span.id.toString());
          //#region variables switching
          let help;
          let rownumber1;
          let rownumber2;
          let columnnumber1;
          let columnnumber2;
          //#endregion

          for (var row = 0; row < arrayNumbers.length; row++) {
            for (var column = 0; column < arrayNumbers.length; column++) {
              if (
                arrayNumbers[row][column].id ==
                localStorage.getItem("chosennumber1")
              ) {
                rownumber1 = row;
                columnnumber1 = column;
              }
              if (
                arrayNumbers[row][column].id ==
                localStorage.getItem("chosennumber2")
              ) {
                rownumber2 = row;
                columnnumber2 = column;
              }
            }
          }
          help = arrayNumbers[rownumber2][columnnumber2];

          arrayNumbers[rownumber2][columnnumber2] =
            arrayNumbers[rownumber1][columnnumber1];
          arrayNumbers[rownumber1][columnnumber1] = help;

          localStorage.removeItem("chosennumber1");
          localStorage.removeItem("chosennumber2");

          reloadRaster();
        }
      });
      if (this.columnNumber == 1) {
        cell1 = element.id;
        span.dataset.column = "1";
        span.dataset.row = rownumber.toString();

        cell1 = span;
        this.columnNumber = 2;
      } else if (this.columnNumber == 2) {
        span.dataset.row = rownumber.toString();
        span.dataset.column = "2";

        cell2 = span;
        this.columnNumber = 3;
      } else {
        span.dataset.row = rownumber.toString();
        span.dataset.column = "3";

        cell3 = span;
        this.columnNumber = 1;
        rownumber++;

        arrayNumbers.push([cell1, cell2, cell3]);
      }
    });
    reloadRaster();
    function reloadRaster() {
      console.log("erin");
      //columns
      let column1 = document.getElementById("column1");
      let column2 = document.getElementById("column2");
      let column3 = document.getElementById("column3");

      column1.innerHTML = "";
      column2.innerHTML = "";
      column3.innerHTML = "";
      for (var row = 1; row < arrayNumbers.length + 1; row++) {
        for (var column = 1; column < arrayNumbers.length + 1; column++) {
          arrayNumbers[row - 1][column - 1].style.backgroundColor =
            "transparent";
          if (row - 1 == 0) {
            column1.appendChild(arrayNumbers[row - 1][column - 1]);
          }
          if (row - 1 == 1) {
            column2.appendChild(arrayNumbers[row - 1][column - 1]);
          }
          if (row - 1 == 2) {
            column3.appendChild(arrayNumbers[row - 1][column - 1]);
          }
        }
      }
    }
  }
}
