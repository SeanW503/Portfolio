using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check
{
    public class Processor
    {
        public String number;
        private String [] sets = {"", "Thousand ", "Million ", "Billion ", "Trillion ", "Quadrillion ", "Quintillion ", "Sextillion ", "Septillion ", "Octillion ", "Nonillion ", "Decillion "};
        private String [] hundreds = {"", "One Hundred ", "Two Hundred ", "Three Hundred ", "Four Hundred ", "Five Hundred ", "Six Hundred ", "Seven Hundred ", "Eight Hundred ", "Nine Hundred "};
        private String [] tens = {"", "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};
        private String [] teens = {"", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "};
        private String [] ones = {"", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine "};
        private char [] comp = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        public String words = "";
        public Processor(String number){
            this.number = number;
            if (number[number.Length - 3] == '.')
            {
                makeArrays();
            } else
            {
                words = "Invalid Input";
            }
        }

        public void makeArrays(){
            char [] numArray = new char[number.Length - 3];
            char [] decArray = new char[3];
            int decCount = 0;
            for(int i = 0; i < number.Length - 3; i++){
                numArray[i] = number[i];
            }
            for(int j = number.Length - 3; j < number.Length; j++){
                decArray[decCount] = number[j];
                decCount++;
            }
            if(numArray.Length == 1 && numArray[0] == '0'){
                words = "Zero ";
                addDec(decArray);
            } else{
                splitsArray(numArray, 0, numArray.Length);
                addDec(decArray);
            }
        }

        public void splitsArray(char [] numArray, int start, int end){
            int numArraySize = ((end - 1)/3);
            int splitCount = 0;
            char [] splitArray = new char[3];
            if(numArraySize > 0){
                if(end%3 == 0){
                    for(int i = start; i < start + 3; i++){
                        splitArray[splitCount] = numArray[i];
                        splitCount++;
                    }
                    convertArray(splitArray, numArraySize);
                    splitsArray(numArray, start + 3, end - 3);
                } else if (end%3 == 1){
                    splitArray[2] = numArray[start];
                    convertArray(splitArray, numArraySize);
                    splitsArray(numArray, start + 1, end - 1);
                } else if (end%3 == 2){
                    splitArray[1] = numArray[start];
                    splitArray[2] = numArray[start + 1];
                    convertArray(splitArray, numArraySize);
                    splitsArray(numArray, start + 2, end - 2);
                }
            } else {
                if(end%3 == 0){
                    for(int i = start; i < start + 3; i++){
                        splitArray[splitCount] = numArray[i];
                        splitCount++;
                    }
                } else if (end%3 == 1){
                    splitArray[2] = numArray[start];
                } else if (end%3 == 2){
                    splitArray[1] = numArray[start];
                    splitArray[2] = numArray[start + 1];
                }
                convertArray(splitArray, numArraySize);
            }
        }

        public void convertArray(char [] splitArray, int set){
            String [] word = new String[3];
            for(int k = 0; k < word.Length; k++){
                word[k] = "";
            }
            for(int i = 0; i < 3; i++){
                switch(i){
                    case 0:
                        for(int j = 0; j < comp.Length; j++){
                            if(splitArray[i] == comp[j]){
                                word[i] = hundreds[j];
                            }
                        }
                    break;
                    case 1:
                        for(int j = 0; j < comp.Length; j++){
                            if(splitArray[i] == comp[j]){
                                if (splitArray[i + 1] != '0' && splitArray[i] != '0' && splitArray[i] != '1') {
                                    word[i] = tens[j] + "-";
                                } else if (splitArray[i] == '1' && splitArray[i + 1] != '0') {
                                    word[i] = "";
                                } else if (splitArray[i] == '0' && splitArray[i + 1] != '0') {
                                    word[i] = tens[j];
                                } else if (splitArray[i] == '0' && splitArray[i +1] == '0') {
                                    word[i] = "";
                                } else {
                                    word[i] = tens[j] + " ";
                                }
                            }
                        }
                    break;
                    case 2:
                        for(int j = 0; j < comp.Length; j++){
                            if(splitArray[i] == comp[j]){
                                if(splitArray[i-1] == '1' && splitArray[i] != '0'){
                                    word[i] = teens[j];
                                } else {
                                    word[i] = ones[j];
                                }
                            }
                        }
                    break;
                }
            }
            buildString(word, set);
        }

        public void buildString(String [] word, int set){
            if(word[0].Equals("")){
                words = words + word[0] + word[1] + word[2] + sets[set];
            } else {
                if(word[1].Equals("")){
                    if(word[2].Equals("")){
                        words = words + word[0] + sets[set];
                    } else {
                        words = words + word[0] + "and " + word[2] + sets[set];
                    }
                } else {
                    words = words + word[0] + "and " + word[1] + word[2] + sets[set];
                }
            }
        }

        public void addDec(char [] decArray){
            words = words + "and " + decArray[1] + decArray[2] + "/100";
        }

        public String getWord(){
            return words;
        }
    }
}
