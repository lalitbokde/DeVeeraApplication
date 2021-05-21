function GetDiaryByDate() {
    debugger
    var date = document.getElementById("date").value;

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
                    <label class="text-theme-21 star">Add New Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ...">${data.comment}</div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("savediary").style.display = "block";

        }
        else if (data == null && selectedDate == today) {
            var html = `<label class="text-theme-21 star">Title </label> <br /><div class="editable" id="title" name="title" contenteditable spellcheck="false" data-placeholder="Enter Title Here ..."></div><br /><br />
                    <label class="text-theme-21 star">Add New Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ..."></div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("savediary").style.display = "block";
        }
        else if (data != null && selectedDate == today) {
            var html = `<label class="text-theme-21 star">Title </label> <br /><div class="editable" id="title" name="title" contenteditable spellcheck="false" data-placeholder="Enter Title Here ...">${data.title}</div><br /><br />
                    <label class="text-theme-21 star">Add New Diary Entry  </label><br />
                    <div div class="editable" id="comment" name="description" contenteditable spellcheck="false" data-placeholder="Enter Description Here ...">${data.comment}</div>`
            diaryContent.insertAdjacentHTML('beforeend', html);
            document.getElementById("savediary").style.display = "block";
        }
        else
        {
            document.getElementById("savediary").style.display = "none";
        }

        document.getElementById("Id").value = data==null ? 0 :data.id;

    })
} 

function getDiaryContent() {
    debugger
    var title = document.getElementById("title");
    title = title == null ? "" : title.innerHTML
    var comment = document.getElementById("comment");
    comment = comment == null ? "" : comment.innerHTML
    document.getElementById("Title").value = title;
    document.getElementById("Comment").value = comment;
}

function getDiaryColor(ColorCode) {
    debugger
    document.getElementById("diaryPaper").style.backgroundColor = ColorCode
    document.getElementById("lines").setAttribute('style', `background-image:repeating-linear-gradient(${ColorCode} 0px, ${ColorCode} 24px, steelblue 25px)`)
    document.getElementById("DiaryColor").value = ColorCode;
}