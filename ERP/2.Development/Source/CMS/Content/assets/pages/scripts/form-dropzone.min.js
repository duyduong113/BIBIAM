//var FormDropzone = function () {
//    return {
//        init: function () {
//            Dropzone.options.myDropzone =
//              {
//                  dictDefaultMessage: "",
//                  init: function () {
//                      this.on("addedfile", function (e) {
//                          var n = Dropzone.createElement("<a href='javascript:;'' class='btn red btn-sm btn-block'>Remove</a>"), t = this;
//                          var caption = Dropzone.createElement('<textarea class="form-control galleryCaption" rows="2" placeholder="Caption here"></textarea>')
//                          var radio = Dropzone.createElement('<span style="font-size:13px;"><input type="radio" name="galleryCover" class="galleryCover"/> Make cover</span>')
//                          n.addEventListener("click", function (n) {
//                              n.preventDefault(), n.stopPropagation(), t.removeFile(e)
//                          }),
//                          e.previewElement.appendChild(caption),
//                          e.previewElement.appendChild(radio),
//                          e.previewElement.appendChild(n)
//                      })
//                  }
//              }
//        }
//    }
//}(); jQuery(document).ready(function () { FormDropzone.init() });