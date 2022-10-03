<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading... 
        </div>

        <div v-if="categories" class="content">
            <table class="table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Nepali Name. (C)</th>
                        <th>Description. (F)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="category in categories" :key="category.id">
                        <td>{{ category.name }}</td>
                        <td>{{ category.nepaliName }}</td>
                        <td>{{ category.description }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            this.fetchData();
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('/api/categories')
                    .then(r => r.json())
                    .then(json => {
                        this.categories = json;
                        this.loading = false;
                        return;
                    });
            }
        },
    });
</script>