let count = 1;

const uniqId = () => {
    return Math.random().toString(16).slice(2)
}

const addChoice = (e,formChoices,id,prefix = "PollFields") => {
    count++
    formChoices.append(`
            <div id="${prefix}${id}" class="form-group form-group-icon">
                <input type="text" class="form-field" name="${prefix}">
                <a data-choice-id="${prefix}${id}" href="#" class="delete-choice" disabled="">
                    <i class="icon-DeleteIcon"></i>
                </a>
            </div>
       `)
    $(`#${prefix}${id} .delete-choice`).on('click', (e) => removeChoice(e))
}

const removeChoice = (e) => {
    e.preventDefault()
    console.log($('.delete-choice'))
    if (count !== 1){
        count--
        let choiceId = $(e.currentTarget).data('choiceId')
        $(`#${choiceId}`).remove()  
    }
}

$(document).ready(function()
{
   const formChoices = $('.form-choices')
   const max = formChoices.data('maxChoices') ? formChoices.data('maxChoices') : 5;
   $('.delete-choice').on('click', (e) => removeChoice(e))
   count =  $('.form-choices .form-field').length
   $('.add-more-choice-btn').on('click', (e) =>{
       e.preventDefault()
       if (count < max)
            addChoice(e,formChoices,uniqId())
   })
});