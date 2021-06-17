function GetDiaryByDate(date) {
    debugger
    if (date == null){
        var date = document.getElementById("date").value;
    }else{
        var date = date;
    }
    var selectedDate = new Date(date);
    var sMonth = selectedDate.getMonth() + 1;
    var sDay = selectedDate.getDate();
    var sYear = selectedDate.getFullYear();

    selectedDate = sMonth + "/" + sDay + "/" + sYear;

    var today = new Date();
    var tMonth = today.getMonth() + 1;
    var tDay = today.getDate();
    var tYear = today.getFullYear();

    today = tMonth + "/" + tDay + "/" + tYear;

    var jsondata = { Date: date };

    $.post("/Diary/GetDiaryByDate", jsondata, function (data) {
        debugger
        document.getElementById("diaryContent").innerHTML = "";
        var diaryContent = document.getElementById("diaryContent");
        if (data != null && selectedDate != today) {
            var html = `<label class="text-theme-21 star">Title </label> <br /><div class="editable" id="title" name="title" contenteditable spellcheck="false" data-placeholder="Enter Title Here ...">${data.title}</div><br /><br />
                    <label class="text-theme-21 star"> Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ...">${data.comment}</div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("diaryPaper").style.backgroundColor = data.diaryColor;
            document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient(${data.diaryColor} 0px, ${data.diaryColor} 24px, steelblue 25px)`);
            document.getElementById("savediary").style.display = "block";

            var createdDate = new Date(data.createdOn);
            var cMonth = createdDate.toLocaleString('default', { month: 'long' });
            var cDay = createdDate.getDate();
            var cYear = createdDate.getFullYear();
            createdDate = `${cMonth} ${cDay}, ${cYear}`;
            document.getElementById("date").value = createdDate;
        }
        else if (data == null ) {
            var html = `<label class="text-theme-21 star">Title </label> <br /><div class="editable" id="title" name="title" contenteditable spellcheck="false" data-placeholder="Enter Title Here ..."></div><br /><br />
                    <label class="text-theme-21 star"> Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ..."></div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("diaryPaper").setAttribute('style', `background-color : white`);
            document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient( 0px, 24px, steelblue 25px)`);
            document.getElementById("savediary").style.display = "block";
        }
        else if (data != null && selectedDate == today) {
            var html = `<label class="text-theme-21 star">Title </label> <br /><div class="editable" id="title" name="title" contenteditable spellcheck="false" data-placeholder="Enter Title Here ...">${data.title}</div><br /><br />
                    <label class="text-theme-21 star"> Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ...">${data.comment}</div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("diaryPaper").setAttribute('style', `background-color:${data.diaryColor}`);
            document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient(${data.diaryColor} 0px, ${data.diaryColor} 24px, steelblue 25px)`);
            document.getElementById("savediary").style.display = "block";

            var createdDate = new Date(data.createdOn);
            var cMonth = createdDate.toLocaleString('default', { month: 'long' });
            var cDay = createdDate.getDate();
            var cYear = createdDate.getFullYear();
            createdDate = `${cMonth} ${cDay}, ${cYear}`;
            document.getElementById("date").value = createdDate;
        }
        //else
        //{
        //    document.getElementById("diaryPaper").setAttribute('style', `background-color : white`);
        //    document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient( 0px, 24px, steelblue 25px)`);
        //    document.getElementById("savediary").style.display = "block";
        //}

        document.getElementById("Id").value = data==null ? 0 :data.id;

    })

} 

function getDiaryContent() {
    debugger
    var title = document.getElementById("title");
    title = title == null ? "" : title.innerHTML
    var comment = document.getElementById("comment");
    comment = comment == null ? "" : comment.innerHTML
    if (title == "" || comment == "") {
        swal({
            type: 'info',
            title: 'Empty diary can not be saved!',
            text: 'Please fill your diary notes!',
            buttonsStyling: false,
            confirmButtonClass: 'btn btn-lg btn-info'
            
        });
        return false;
    }
    else {
        document.getElementById("Diary_Title").value = title;
        document.getElementById("Diary_Comment").value = comment;
        return true;
    }


}

function getDiaryColor(ColorCode) {
    debugger
    document.getElementById("diaryPaper").style.backgroundColor = ColorCode
    document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient(${ColorCode} 0px, ${ColorCode} 24px, steelblue 25px)`)
    document.getElementById("Diary_DiaryColor").value = ColorCode;
}


function MMDDYYYY(value, event) {
        debugger
        let newValue = value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');

        const dayOrMonth = (index) => index % 2 === 1 && index < 4;

        // on delete key.
        if (!event.data) {
            return value;
        }

        return newValue.split('').map((v, i) => dayOrMonth(i) ? v + '/' : v).join('');;
    }
